$(function () {
    var l = abp.localization.getResource('Products');
    function GetPagingData(input) {
        return abp.ajax($.extend(true, {
            url: '?handler=Data',
            type: 'POST',
            data: JSON.stringify(input)
        }));
    }
    function extraFilters() {
        return { categoryId: $("#categoryId").val() != "" && $("#categoryId").val() ? $("#categoryId").val() :null   };
    }
    var dataTable = $('#ProductsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,

            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(GetPagingData, extraFilters),
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
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
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
              
            ]
        })
    );
   
});

function triggerFilter() {
    $('#ProductsTable').DataTable().draw();
}


