$(() => {

    const addModal = new bootstrap.Modal($('#add-modal')[0]);
    const editModal = new bootstrap.Modal($('#edit-modal')[0]);

    function refreshTable() {
        $("tbody").empty();
        $.get('/home/getpeople', function (people) {
            people.forEach(function (person) {
                $("tbody").append(`<tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>
            <td> <button id="myDeleteBtn" data-person-id="${person.id}"  class="btn btn-primary delete">Delete</button> </td>
            <td> <button id="myEditBtn" data-person-id="${person.id}"  class="btn btn-primary edit">Edit</button> </td>


</tr>`)
            });
        });
    }

    refreshTable();

    $("#add-person").on('click', function () {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        addModal.show();
    });

    $("#save-add").on('click', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();


        $.post('/home/addperson', { firstName, lastName, age }, function () {
            addModal.hide();
            refreshTable();
        });

    });

    $("tbody").on('click', '#myDeleteBtn', function () {
        const id = $(this).data('person-id');
        $.post('/home/delete', { id }, function () {
            refreshTable();
        });
    });
    $("tbody").on('click', '#myEditBtn', function () {
        const id = $(this).data('person-id');
        $.get('/home/getSpecificPerson', { id }, function (person) {

            $('#editfirstName').val(person.firstName);
            $('#editlastName').val(person.lastName);
            $('#editage').val(person.age);
            $('#editId').val(person.id);
            editModal.show();

        })
    })
    $("#save-edit").on('click', function () {

        const firstName = $("#editfirstName").val();
        const lastName = $("#editlastName").val();
        const age = $("#editage").val();
        const id = $("#editId").val();
        



        $.post('/home/edit', {firstName, lastName, age, id}, function () {
            editModal.hide();
            refreshTable();
        });

    });

})
