$(function () {
    var l = abp.localization.getResource('BookStore');
    var createModal = new abp.ModalManager(abp.appPath + 'Movies/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Movies/EditModal');

    var dataTable = $('#MoviesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.bookStore.movies.movie.getList),
            columnDefs: [
                {
                    title: l('ACTIONS'),
                    rowAction: {
                        items: [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('BookStore.Movies.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('BookStore.Movies.Delete'),
                                confirmMessage: function (data) {
                                    return l('MovieDeletionConfirmationMessage', data.record.name);
                                },
                                action: function (data) {
                                    acme.bookStore.movies.movie
                                        .delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Type'),
                    data: "type",
                    render: function (data) {
                        return l('Enum:MovieType.' + data);
                    }
                },
                {
                    title: l('DIRECTOR'),
                    data: "director"
                },
                {
                    title: l('IMDB RATINGS'),
                    data: "imdbRatings",
                    render: function (data) {  // Veri kullanýlarak bir HTML çýktýsý oluþturulur
                        return (parseFloat(data) / 10).toFixed(1);
                 
                    }
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewMovieButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
