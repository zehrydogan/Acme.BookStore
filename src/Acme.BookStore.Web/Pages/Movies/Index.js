$(function () {
    var l = abp.localization.getResource('BookStore');
    var createModal = new abp.ModalManager(abp.appPath + 'Movies/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Movies/EditModal');
    var movieCommentModal = new abp.ModalManager(abp.appPath + 'MovieComments/MovieCommentModal');
    var viewCommentModal = new abp.ModalManager(abp.appPath + 'Movies/ViewCommentModal')

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
                    title: l('Actions'),
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
                                text: l('Comment'),
                                visible: abp.auth.isGranted('BookStore.Movies.Comment'),
                                action: function (data) {
                                    movieCommentModal.open({ movieId: data.record.id });
                                }
                            },
                            {
                                text: l('ViewComments'),
                                visible: abp.auth.isGranted('BookStore.Movies.Comment'),
                                action: function (data) {
                                    viewCommentModal.open({ movieId: data.record.id });
                                }
                            },
                            {
                                text: l('AddToWatchList'),
                                visible: abp.auth.isGranted('BookStore.Movies.Comment'),
                                action: function (data) {
                                    debugger
                                    acme.bookStore.userMovies.userMovie
                                        .create({movieId: data.record.id, userId: abp.currentUser.id})
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyAdded'));
                                            dataTable.ajax.reload();
                                        });
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
                    title: l('Actors'),
                    data: "actors",
                    render: function (data) {
                        var names = data.map((a) => a.name);
                        return names.join(', ');
                    }
                },
                {
                    title: l('Type'),
                    data: "type",
                    render: function (data) {
                        return l('Enum:MovieType.' + data);
                    }
                },
                {
                    title: l('Director'),
                    data: "directorName"
                },
                {
                    title: l('IMDBRating'),
                    data: "imdbRatings",
                    //render: function (data) {  // Veri kullanılarak bir HTML çıktısı oluşturulur
                    //    return (parseFloat(data)/10).toFixed(1);
                    //}
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
