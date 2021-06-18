$(function () {
    var l = abp.localization.getResource('Products');
    var createModal = new abp.ModalManager(abp.appPath + 'Categories/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Categories/EditModal');

    var dataTable = $('#CategoriesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(aCME.products.categories.categories.getList),
            columnDefs: [
                {
                    title: l('ID'),
                    data: "id"
                },
                {
                    title: l('CategoryName'),
                    data: "categoryName"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('Picture'),
                    data: "picture",
                    render: function (data) {
                        return `<img src="${data ? 'data:image/png;base64,' + data : '/placeholders/th.jpg'}" width="50" ></img>`;
                    }
                },
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('Products.Categories.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('Products.Categories.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'CategoryDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        aCME.products.categories.categories
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                //{
                //    render: function (data) {
                //        return luxon
                //            .DateTime
                //            .fromISO(data, {
                //                locale: abp.localization.currentCulture.name
                //            }).toLocaleString();
                //    }
                //}
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCategoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

   
});
function triggerFileInput (inputName) {
    $(`[name="${inputName}"]`).click();
}
function clearFileInput(fileInputName) {
    try {
        $(`[name="${fileInputName}"]`).val('');
    } catch (ex) { }
}
function loadImagePreview(fileInputName, imgTagId, imgDefaultValue) {
    const imgPreview = document.getElementById(imgTagId);
    const chooseFile = document.getElementsByName(fileInputName)[0];
    const files = chooseFile.files[0];
    if (files) {
        const fileReader = new FileReader();
        fileReader.readAsDataURL(files);
        fileReader.addEventListener("load", function () {
            imgPreview.setAttribute('src', this.result);
        });
    }
    else {
        imgPreview.setAttribute('src', imgDefaultValue);
    }
}