﻿let Directory = "";
let Directories = [""];
let ArryRemoveItem = [];

$(document).ready(function () {
    getDirectoryList();
});
//Go To Root
function goToRoot() {
    $("#directory-item").html(loading);
    $('#back-button').addClass("d-none");
    $('#remove-files').addClass("d-none");

    Directory = "";
    getDirectoryList();
    Directories = [""];
}

//Back To Last Directory
function backToLastDirectory() {
    $("#directory-item").html(loading);
    $('#remove-files').addClass("d-none");
    Directories.pop();
    //fill new directory
    if (Directories.length > 0) {
        Directory = Directories.reduce(function (a, b) {
            return a + b
        });
        if (Directories.length == 1) {
            $('#back-button').addClass("d-none");
        }
    }
    getDirectoryList();
}

//Open Folder
function openFile(path) {
    $("#directory-item").html(loading);
    $('#back-button').removeClass("d-none");
    $('#remove-files').addClass("d-none");

    Directories.push(path);
    Directory += path;
    getDirectoryList();
}

//Upload files
function uploadSelect() {
    let input = document.createElement('input');
    //input.accept = 'application/pdf';
    input.multiple = true,
        input.type = 'file';
    input.onchange = () => {
        let files = input.files;
        FrmData = new FormData();
        for (var i = 0; i < files.length; i++) {
            FrmData.append("Files", files[i]);
        }
        FrmData.append("Directory", Directory);
        //upload ajax
        $.ajax({
            type: 'POST',
            url: 'UploadFiles',
            data: FrmData,
            contentType: false,
            processData: false,
            async: true,
            cache: false,
            xhr: function () {
                $('.progress').removeClass("d-none");
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener('progress', function (e) {
                    if (e.lengthComputable) {
                        var percentage = Math.round((e.loaded * 100) / e.total);
                        $('.progress .progress-bar').css('width', percentage + '%');
                        $('.progress .progress-bar').text(percentage + '%');
                    }
                }, false);
                return xhr;
            },
            success: function (response) {
                if (response.isSuccess == true) {
                    $('.progress').addClass("d-none");
                    successToastify.showToast();
                    getDirectoryList();
                } else {
                    //error
                    $('.progress').addClass("d-none");
                    dangerToastify.showToast();
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    input.click();
}

//Select For Remove Files Or Directoory
$(document).on('click', '.form-check-input', function (e) {
    $('#remove-files').addClass("d-none");
    ArryRemoveItem = [];
    let directoryItem = $('#directory-item').children();
    $("input:checkbox[name=item]:checked").each(function (index, value) {
        ArryRemoveItem.push($(value).attr('data-name'));
    });
    if (ArryRemoveItem.length > 0) {
        $('#remove-files').removeClass("d-none");
        $('#remove-files').text(`Remove(${ArryRemoveItem.length})`);
    }
});

//Remove Files Or Directoory
$(document).on('click', '#remove-files', function () {
    Swal.fire({
        title: 'Do you want to remove?',
        Text: 'Delete This and all chilren.',
        showDenyButton: true,
        confirmButtonText: 'Yes',
        denyButtonText: `No`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $("#directory-item").html(loading);
            $('#remove-files').addClass("d-none");
            let model = {
                ArryRemoveItem, Directory
            }
            ajaxFunc("RemoveFiles", model, "POST",
                function (result) {
                    if (result.isSuccess) {
                        successToastify.showToast();
                        getDirectoryList();
                    } else {
                        $('#remove-files').removeClass("d-none");
                        $('#notify-text-failed').text(result.message);
                        dangerToastify.showToast();
                        getDirectoryList();
                        console.log(result.message);
                    }
                },
                function (error) {
                    console.log(error);
                }
            );
        } else if (result.isDenied) {
        }
    });
});

//create Directory
$('#create-directory').on('click', function () {
    var Name = prompt("Please enter directory name");
    if (Name.trim().length > 0) {
        $("#directory-item").html(loading);
        var model = { Directory, Name };
        ajaxFunc("CreateDirectory", model, "POST",
            function (result) {
                if (result.isSuccess) {
                    successToastify.showToast();
                    getDirectoryList();
                } else {
                    $('#notify-text-failed').text(result.message);
                    dangerToastify.showToast();
                    getDirectoryList();
                    console.log(result.message);
                }
            },
            function (error) {
                console.log(error);
            }
        );
    }

});

//Get Files
function getDirectoryList() {
    var model = {
        Directory
    }
    console.log(Directories);
    ajaxFunc("GetDirectory", model, "POST",
        function (result) {
            if (result.isSuccess) {
                let html = "";
                if (result.data.length > 0) {
                    result.data.map(item => {
                        html += `<div class="intro-y col-span-6 sm:col-span-4 md:col-span-3 2xl:col-span-2">
                                                        <div  class="file box rounded-md px-5 pt-8 pb-5 px-3 sm:px-5 relative zoom-in">
                                                    <div class="absolute left-0 top-0 mt-3 ml-3">
                                                                <input data-name="${item.name}" name="item" class="form-check-input border border-slate-500" type="checkbox">
                                                    </div>`;
                        if (item.fileType == 0) {
                            html += `<div onclick="openFile('${item.directory}')"  class="w-3/5 file__icon file__icon--directory mx-auto"></div>`;

                        }
                        else if (item.fileType == 1) {
                            html += `<a href="#" class="w-3/5 file__icon file__icon--image mx-auto">
                                                                <div class="file__icon--image__preview image-fit">
                                                                    <img alt="${item.name}" src="${item.baseUrl}${item.path}">
                                                                </div>
                                                            </a>`;
                        }
                        else {
                            html += `<a href="#" class="w-3/5 file__icon file__icon--file mx-auto">
                                                                <div class="file__icon__file-name">${item.postfix}</div>
                                                            </a>`;
                        }


                        html += `<a href="" class="block font-medium mt-4 text-center truncate">${item.name}</a>
                                                    <div class="text-slate-500 text-xs text-center mt-0.5">${item.size}</div>
                                                    <div class="absolute top-0 right-0 mr-2 mt-3 dropdown ml-auto">
                                                        <a class="dropdown-toggle w-5 h-5 block" href="javascript:;" aria-expanded="false" data-tw-toggle="dropdown"> <i data-lucide="more-vertical" class="w-5 h-5 text-slate-500"></i> </a>
                                                        <div class="dropdown-menu w-40">
                                                            <ul class="dropdown-content">
                                                                <li>
                                                                    <a href="" class="dropdown-item"> <i data-lucide="users" class="w-4 h-4 mr-2"></i> Share File </a>
                                                                </li>
                                                                <li>
                                                                    <a href="" class="dropdown-item"> <i data-lucide="trash" class="w-4 h-4 mr-2"></i> Delete </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>`;
                    });
                } else {
                    html = `<div class="intro-y text-center col-span-12 sm:col-span-12 md:col-span-12 2xl:col-span-12">This folder is empty</div>`;
                }
                $("#directory-item").html(html);
            } else {
                console.log("error load");
            }
        },
        function (error) {
            console.log(error);
        }
    );
}
let loading = `<div class="intro-y col-span-12 sm:col-span-12 md:col-span-12 2xl:col-span-12">
                                <div class="col-span-6 sm:col-span-3 xl:col-span-2 flex flex-col justify-end items-center">
                                    <svg width="25" viewBox="0 0 44 44" xmlns="http://www.w3.org/2000/svg" stroke="rgb(30, 41, 59)" class="w-8 h-8">
                                        <g fill="none" fill-rule="evenodd" stroke-width="4">
                                            <circle cx="22" cy="22" r="1">
                                                <animate attributeName="r" begin="0s" dur="1.8s" values="1; 20" calcMode="spline" keyTimes="0; 1" keySplines="0.165, 0.84, 0.44, 1" repeatCount="indefinite"></animate>
                                                <animate attributeName="stroke-opacity" begin="0s" dur="1.8s" values="1; 0" calcMode="spline" keyTimes="0; 1" keySplines="0.3, 0.61, 0.355, 1" repeatCount="indefinite"></animate>
                                            </circle>
                                            <circle cx="22" cy="22" r="1">
                                                <animate attributeName="r" begin="-0.9s" dur="1.8s" values="1; 20" calcMode="spline" keyTimes="0; 1" keySplines="0.165, 0.84, 0.44, 1" repeatCount="indefinite"></animate>
                                                <animate attributeName="stroke-opacity" begin="-0.9s" dur="1.8s" values="1; 0" calcMode="spline" keyTimes="0; 1" keySplines="0.3, 0.61, 0.355, 1" repeatCount="indefinite"></animate>
                                            </circle>
                                        </g>
                                    </svg>
                                    <div class="text-center text-xs mt-2">Please Wait...</div>
                                </div>
                                </div>`;