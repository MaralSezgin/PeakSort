$(document).ready(function () {
    $('#usersTable').DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({

                        type: 'GET',
                        url: '/Admin/User/GetAllCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            /* $('#categoriesTable').hide();*/
                            /* $('.spinner-border').show();*/
                        },
                        success: function (data) {
                            /* burası eksik */
                        },
                        error: function () {

                        },
                    });


                }
            }
        ]
    });


    /* dataTable burada bitiyor */
    $(function () {

        const placeHolder = $('#modelPlaceHolder');
        const url = '/Admin/User/Add/';

        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolder.html(data);
                placeHolder.find(".modal").modal('show');
            })
        });

        placeHolder.on('click', '#btnSave',
            function (event) {
                event.preventDefault();

                const form = $('#form-category-add');

                const urlAction = form.attr('action');
                const dataSend = form.serialize();


                $.post(urlAction, dataSend).done(function (data) {

                    const categoryAddAjaxModel = jQuery.parseJSON(data);




                    const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAddPartial);
                    placeHolder.find('.modal-body').replaceWith(newFormBody);

                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                    if (isValid) {
                        placeHolder.find('.modal').modal('hide');
                        const newTableRow = `
                          <tr name=${categoryAddAjaxModel.CategoryDto.Category.CategoryID}>
                            <td>${categoryAddAjaxModel.CategoryDto.Category.CategoryID}</td>
                            <td>${categoryAddAjaxModel.CategoryDto.Category.CategoryName}</td>
                            <td>${categoryAddAjaxModel.CategoryDto.Category.Description}</td>
                            <td>${categoryAddAjaxModel.CategoryDto.Category.Note}</td>
                            <td>${categoryAddAjaxModel.CategoryDto.Category.IsActive}</td>
                            <td>${categoryAddAjaxModel.CategoryDto.Category.CreatedByName}</td>
                            <td>${categoryAddAjaxModel.CategoryDto.Category.ModifiedByName}</td>
                            <td>${converToShortDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                            <td>${converToShortDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                            <td>
                            <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.CategoryID}"><span class="fas fa-minus-circle"></span></button>
                            <button class="btn btn-primary btn-sm btn-update" data-id="${categoryAddAjaxModel.CategoryDto.Category.CategoryID}"><span class="fas fa-edit "></span></button>
                           </td>
                         <tr>`
                        const newTableObject = $(newTableRow);
                        newTableObject.hide();
                        $('#categoriesTable').append(newTableObject);
                        newTableObject.fadeIn(3000);
                        toastr.success(`${categoryAddAjaxModel.CategoryDto.Category.CategoryName}`, 'Başarıyla eklendi');

                    }
                    else {

                        let warningText = "";
                        $('#validation-summary>ul>li').each(function () {

                            let text = $(this).text();
                            warningText = `*${text}\n`;
                        });
                        toastr.warning(warningText);

                    }


                });

            });

        /* Start delete*/

        $(document).on('click', '.btn-delete', function () {
            const id = $(this).attr('data-id');
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: "Seçili kategori silinecektir!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet,Silmek istiyorum!',
                cancelButtonText: 'Hayır,Silmek istemmiyorum!',
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { categoryId: id },
                        url: '/Admin/Category/Delete/',

                        success: function (data) {
                            const result = jQuery.parseJSON(data);
                            if (result.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    'Seçili kategori başarıyla silindi.',
                                    'success'
                                )
                                const tableRow = $(`[name="${id}"]`);
                                tableRow.fadeOut(2500);
                            }
                            else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Oops...',
                                    text: 'Birşeyler yanlış gitti',
                                })
                            }
                        },
                        error: function () {

                        },

                    });
                }
            })

        });
    });

    $(function () {
        const url = '/Admin/Category/Update/';
        const placeHolderDiv = $('#modelPlaceHolder');

        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
           
            $.get(url, { categoryId: id }).done(function (data) {
             
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');

            }).fail(function () {

                toastr.error("Bir hata oluştu");
            });
        });

        /* Ajax POST / Updating a Category start from here*/

        $(document).on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-category-update');
            const urlAction = form.attr('action');

            const sendToData = form.serialize();
        

            $.post(urlAction, sendToData).done(function (data) {

                const categoryAjaxUpdateModel = jQuery.parseJSON(data);
                console.log(categoryAjaxUpdateModel);
                const newFormBody = $('.modal-body', categoryAjaxUpdateModel.CategoryUpdatePartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);

                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

            
                if (isValid)
                {
                  
                    placeHolderDiv.find('.modal').modal('hide');

                    const newTableRow = `
                     <tr name="${categoryAjaxUpdateModel.CategoryDto.Category.CategoryID}">
                            <td>${categoryAjaxUpdateModel.CategoryDto.Category.CategoryID}</td>
                            <td>${categoryAjaxUpdateModel.CategoryDto.Category.CategoryName}</td>
                            <td>${categoryAjaxUpdateModel.CategoryDto.Category.Description}</td>
                            <td>${categoryAjaxUpdateModel.CategoryDto.Category.Note}</td>
                            <td>${categoryAjaxUpdateModel.CategoryDto.Category.IsActive}</td>
                            <td>${categoryAjaxUpdateModel.CategoryDto.Category.CreatedByName}</td>
                            <td>${categoryAjaxUpdateModel.CategoryDto.Category.ModifiedByName}</td>
                            <td>${converToShortDate(categoryAjaxUpdateModel.CategoryDto.Category.CreatedDate)}</td>
                            <td>${converToShortDate(categoryAjaxUpdateModel.CategoryDto.Category.ModifiedDate)}</td>
                            <td>
                            <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAjaxUpdateModel.CategoryDto.Category.CategoryID}"><span class="fas fa-minus-circle"></span></button>
                            <button class="btn btn-primary btn-sm btn-update" data-id="${categoryAjaxUpdateModel.CategoryDto.Category.CategoryID}"><span class="fas fa-edit "></span></button>
                           </td>
                     <tr>`

                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${categoryAjaxUpdateModel.CategoryDto.Category.CategoryID}"]`);
                    categoryTableRow.replaceWith(newTableRowObject);
                    categoryTableRow.hide();
                    newTableRowObject.hide();
                    categoryTableRow.fadeIn(3500);
                    newTableRowObject.fadeIn(3500);
                    newTableRowObject.css('background-color', '#acd3ff');
                    toastr.success(`${categoryAjaxUpdateModel.CategoryDto.Category.CategoryName }`,"İşlem Başarılı")
                }
                else {
                    toastr.error("Başarısız");
                }
               
            }).fail(function (response) {
             

            });



        });

    


    });

});

