$(document).ready(function () {
   const dataTable= $('#usersTable').DataTable({
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

                const form = $('#form-user-add');

                const urlAction = form.attr('action');
                const dataSend = new FormData(form.get(0));

                $.ajax({

                    url: urlAction,
                    type: 'POST',
                    data: dataSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {

                        const userAddAjaxModel = jQuery.parseJSON(data);
                        const newFormBody = $('.modal-body', userAddAjaxModel.UserAddPartial);
                        placeHolder.find('.modal-body').replaceWith(newFormBody);
                       
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                        if (isValid) {
                            placeHolder.find('.modal').modal('hide');
                           
                            dataTable.row.add([

                                userAddAjaxModel.UserDto.User.Id,
                                userAddAjaxModel.UserDto.User.UserName,
                                userAddAjaxModel.UserDto.User.Email,
                                userAddAjaxModel.UserDto.User.PhoneNumber,
                               
                                `<img src="/img/${userAddAjaxModel.UserDto.User.Picture}" style="max-height:50px; max-width:50px" alt="${userAddAjaxModel.UserDto.User.Picture}" />`,
                                `<td>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="userAddAjaxModel.UserDto.User.id"><span class="fas fa-minus-circle"></span></button>
                                    <button class="btn btn-primary btn-sm btn-update" data-id="userAddAjaxModel.UserDto.User.id"><span class="fas fa-edit "></span></button>
                                </td>`


                            ]).draw();
                            toastr.success(`${userAddAjaxModel.UserDto.User.UserName}`, 'Başarıyla eklendi');

                        }
                        else {

                            let warningText = "";
                            $('#validation-summary>ul>li').each(function () {

                                let text = $(this).text();
                                warningText = `*${text}\n`;
                            });
                            toastr.warning(warningText);

                        }



                    },/*success end*/
                    error: function (err) {
                        console.log(err);
                    },




                });/*ajax end*/

          

            });

        /* Start delete*/

        $(document).on('click', '.btn-delete', function () {
            const id = $(this).attr('data-id');
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: "Seçili kullanıcı silinecektir!",
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
                        data: { userId: id },
                        url: '/Admin/User/Delete/',

                        success: function (data) {
                            const result = jQuery.parseJSON(data);
                            if (result.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    'Seçili kullanıcı başarıyla silindi.',
                                    'success'
                                )
                                const tableRow = $(`[name="${id}"]`);
                                dataTable.row(tableRow).remove().draw();
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
        const url = '/Admin/User/Update/';
        const placeHolderDiv = $('#modelPlaceHolder');

        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            
            
            $.get(url, { userId: id }).done(function (data) {
             
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');

            }).fail(function () {

                toastr.error("Bir hata oluştu");
            });
        });

        /* Ajax POST / Updating a Category start from here*/
     
        $(document).on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-user-update');
            const urlAction = form.attr('action');

            const dataSend = new FormData(form.get(0));

            $.ajax({

                url: urlAction,
                type: 'POST',
                data: dataSend,
                processData: false,
                contentType: false,
                success: function (data) {

                    const userAjaxUpdateModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', userAjaxUpdateModel.UserUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);

                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                    const id = userAjaxUpdateModel.UserDto.User.Id;
                    const tableRow = $(`[name="${id}"]`);
                    
                    if (isValid) {
                    
                        placeHolderDiv.find('.modal').modal('hide');
                       
                        dataTable.row(tableRow).data([

                            userAjaxUpdateModel.UserDto.User.Id,
                            userAjaxUpdateModel.UserDto.User.UserName,
                            userAjaxUpdateModel.UserDto.User.Email,
                            userAjaxUpdateModel.UserDto.User.PhoneNumber,

                            `<img class="my-image-table"  src="/img/${userAjaxUpdateModel.UserDto.User.Picture}" alt="${userAjaxUpdateModel.UserDto.User.Picture}" />`,
                            `
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${userAjaxUpdateModel.UserDto.User.id}"><span class="fas fa-minus-circle"></span></button>
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${userAjaxUpdateModel.UserDto.User.id}"><span class="fas fa-edit "></span></button>
                              `


                        ]);
                        tableRow.attr("name", `${id}`);
                        dataTable.row(tableRow).invalidate();
                        toastr.success(`${userAjaxUpdateModel.UserDto.Message}`, 'Başarıyla eklendi');

                  
                    }
                    else {
                        toastr.error("Başarısız");
                    }


                },
                error: function (error) {
                    console.log(error);
                }



            });
             
        
             

      



        });

    


    });

});

//const newTableRow = `
//                          <tr name=${categoryAddAjaxModel.CategoryDto.Category.CategoryID}>
//                            <td>${categoryAddAjaxModel.CategoryDto.Category.CategoryID}</td>
//                            <td>${categoryAddAjaxModel.CategoryDto.Category.CategoryName}</td>
//                            <td>${categoryAddAjaxModel.CategoryDto.Category.Description}</td>
//                            <td>${categoryAddAjaxModel.CategoryDto.Category.Note}</td>
//                            <td>${categoryAddAjaxModel.CategoryDto.Category.IsActive}</td>
//                            <td>${categoryAddAjaxModel.CategoryDto.Category.CreatedByName}</td>
//                            <td>${categoryAddAjaxModel.CategoryDto.Category.ModifiedByName}</td>
//                            <td>${converToShortDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}</td>
//                            <td>${converToShortDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
//                            <td>
//                            <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.CategoryID}"><span class="fas fa-minus-circle"></span></button>
//                            <button class="btn btn-primary btn-sm btn-update" data-id="${categoryAddAjaxModel.CategoryDto.Category.CategoryID}"><span class="fas fa-edit "></span></button>
//                           </td>
//                         <tr>`