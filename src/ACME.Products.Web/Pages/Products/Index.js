$(function () {
    var l = abp.localization.getResource('Products');
    var createModal = new abp.ModalManager(abp.appPath + 'Products/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Products/EditModal');

    var dataTable = $('#ProductsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(aCME.products.products.products.getList),
            columnDefs: [
                {
                    title: l('ID'),
                    data: "id"
                },
                {
                    title: l('ProductName'),
                    data: "productName"
                },
                {
                    title: l('Price'),
                    data: "price"
                },
                {
                    title: l('Published'),
                    data: "published"
                },
                {
                    title: l('Picture'),
                    data: "picture",
                    render: function (data) {
                        return `<img src="${data ? 'data:image/png;base64,' + data : '/placeholders/th.jpg'}" width="50" ></img>`;
                    }
                },
                {
                    title: l('UnitsInStock'),
                    data: "unitsInStock"
                },
                {
                      title: l('PublishDate'),
                      data: "publishDate",
                    render: function (data) {
                        return data?  luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString() : '-';
                    }
                },
                {
                    title: l('Categories'),
                    data: "categoires",
                    sortable: false,
                    render: function (data, index, full) {
                        return full.categories.reduce((acc, curr) => acc + `<span class="badge badge-secondary m-1">${curr.categoryName}</span>`, "")
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
                                        abp.auth.isGranted('Products.ProductsManagements.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('Products.ProductsManagements.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'ProductDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        aCME.products.products.products
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
              
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });


});
function triggerFileInput(inputName) {
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