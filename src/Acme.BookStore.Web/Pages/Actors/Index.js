$(function () {
    var l = abp.localization.getResource('BookStore');
    var createModal = new abp.ModalManager(abp.appPath + 'Actors/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Actors/EditModal');

    var dataTable = $('#ActorsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.bookStore.actors.actor.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
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
                                        return l('ActorDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        acme.bookStore.actors.actor
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
                    title: l('Gender'),
                    data: "gender",
                    render: function (data) {
                        return l(data);
                    }
                },
                {
                    title: l('BirthDate'),
                    data: "birthDate",
                    render: function (data) {
                        return luxon.DateTime.fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toLocaleString();
                    }
                },
                {
                    title: l('Age'),
                    data: "birthDate",
                    render: function (data) {
                        const birthDate = luxon.DateTime.fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        });
                        const today = luxon.DateTime.now();
                        const age = today.year - birthDate.year - (
                            (today.month < birthDate.month ||
                                (today.month === birthDate.month && today.day < birthDate.day)) ? 1 : 0
                        );
                        return age;
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

    $('#NewActorButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
