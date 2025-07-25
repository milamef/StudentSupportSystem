$(() => {  // main jQuery routine - executes every on page load, $ is short for jquery 
    const getAll = async (msg) => {
        try {
            $("#studentList").text("Finding Student Information...");
            let response = await fetch(`/api/student`);
            if (response.ok) {
                let payload = await response.json(); // this returns a promise, so we await it 
                buildStudentList(payload);
                msg === "" ? // are we appending to an existing message 
                    $("#status").text("Students Loaded") : $("#status").text(`${msg} - Students Loaded`);
            } else if (response.status !== 404) { // probably some other client side error 
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else { // else 404 not found 
                $("#status").text("no such path on server");
            } // else
            // get major data 
            response = await fetch(`/api/major`);
            if (response.ok) {
                let mjrs = await response.json(); // this returns a promise, so we await it 
                sessionStorage.setItem("allmajors", JSON.stringify(mjrs));
            } else if (response.status !== 404) { // probably some other client side error 
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else { // else 404 not found 
                $("#status").text("no such path on server");
            } // else
        } catch (error) {
            $("#status").text(error.message);
        }
    }; // getAll


    const buildStudentList = (data) => {
        $("#studentList").empty();
        div = $(`<div class="list-group-item row d-flex" id="status">Student Info</div> 
                  <div class= "list-group-item row d-flex text-center" id="heading"> 
                  <div class="col-4 h2">Title</div>
                  <div class="col-4 h2">First</div> 
                  <div class="col-4 h2">Last</div> 
               </div>`);
        div.appendTo($("#studentList"));
        sessionStorage.setItem("allstudents", JSON.stringify(data));
        btn = $(`<button class="list-group-item row d-flex" id="0">...click to add student</button>`);
        btn.appendTo($("#studentList"));
        data.forEach(stu => {
            btn = $(`<button class="list-group-item row d-flex" id="${stu.id}">`);
            btn.html(`<div class="col-4" id="studenttitle${stu.id}">${stu.title}</div> 
                      <div class="col-4" id="studentfname${stu.id}">${stu.firstname}</div> 
                      <div class="col-4" id="studentlastnam${stu.id}">${stu.lastname}</div>`
            );
            btn.appendTo($("#studentList"));
        }); // forEach 
    }; // buildStudentList 

    getAll(""); // first grab the data from the server

    $("#studentList").on('click', (e) => {
        if (!e) e = window.event;
        let id = e.target.parentNode.id;
        if (id === "studentList" || id === "") {
            id = e.target.id;
        } // clicked on row somewhere else  
        if (id !== "status" && id !== "heading") {
            let data = JSON.parse(sessionStorage.getItem("allstudents"));
            id === "0" ? setupForAdd() : setupForUpdate(id, data); 
        } else {
            return false; // ignore if they clicked on heading or status 
        }
    }); // studentListClick


    const update = async (e) => { 
        // action button click event handler
        try {
            // set up a new client side instance of Student
            let stu = JSON.parse(sessionStorage.getItem("student"));
            // pouplate the properties
            stu.title = $("#TextBoxTitle").val();
            stu.firstname = $("#TextBoxFirstName").val();
            stu.lastname = $("#TextBoxLastName").val();
            stu.email = $("#TextBoxEmail").val();
            stu.phoneno = $("#TextBoxPhoneNo").val();
            stu.majorId = parseInt($("#ddlMajors").val());
            // send the updated back to the server asynchronously using Http PUT
            let response = await fetch("/api/student", {
                method: "PUT",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(stu),
            });
            if (response.ok) {
                // or check for response.status
                let payload = await response.json();
                $("#status").text(payload.msg);

                getAll(payload.msg); // Pass the message for status

                $("#theModal").modal("toggle");
            } else if (response.status !== 404) {
                // probably some other client side error
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else {
                // else 404 not found
                $("#status").text("no such path on server");
            } // else
        } catch (error) {
            $("#status").text(error.message);
            console.table(error);
        } // try/catch
    }; // update


    const clearModalFields = () => {
        loadMajorDDL(-1);
        $("#TextBoxTitle").val("");
        // clean out the other four text boxes go here as well 
        $("#TextBoxFirstName").val("");
        $("#TextBoxLastName").val("");
        $("#TextBoxPhoneNo").val("");
        $("#TextBoxEmail").val("");
        sessionStorage.removeItem("student");
        $("#theModal").modal("toggle");
    }; // clearModalFields


    const setupForAdd = () => {
        $("#deletebutton").hide();
        $("#dialog").hide();
        $("#actionbutton").val("add");
        $("#modaltitle").html("<h4>add student</h4>");
        $("#theModal").modal("toggle");
        $("#modalstatus").text("add new student");
        $("#theModalLabel").text("Add");
        clearModalFields();
    }; // setupForAdd


    const setupForUpdate = (id, data) => {
        $("#deletebutton").show();
        $("#dialog").hide();
        $("#actionbutton").val("update");
        $("#modaltitle").html("<h4>update student</h4>");
        clearModalFields();
        data.forEach(student => {
            if (student.id === parseInt(id)) {
                $("#TextBoxTitle").val(student.title);
                // populate the other four text boxes here
                $("#TextBoxFirstName").val(student.firstname);
                $("#TextBoxLastName").val(student.lastname);
                $("#TextBoxEmail").val(student.email);
                $("#TextBoxPhoneNo").val(student.phoneno);
                sessionStorage.setItem("student", JSON.stringify(student));
                $("#modalstatus").text("update data");
                $("#theModal").modal("toggle");
                $("#theModalLabel").text("Update");
                loadMajorDDL(student.majorId);
            } // if 
        }); // data.forEach 
    }; // setupForUpdate


    const add = async () => {
        try {
            stu = new Object();
            stu.title = $("#TextBoxTitle").val();
            // populate the other four properties here from the text box contents
            stu.firstname = $("#TextBoxFirstName").val();
            stu.lastname = $("#TextBoxLastName").val();
            stu.email = $("#TextBoxEmail").val();
            stu.phoneno = $("#TextBoxPhoneNo").val();
            stu.majorId = 100; // hard code it for now, we"ll add a dropdown later 
            stu.id = -1;
            stu.timer = null;
            stu.picture64 = null;
            // send the student info to the server asynchronously using POST 
            let response = await fetch("/api/student", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(stu)
            });
            if (response.ok) // or check for response.status  
            {
                let data = await response.json();
                getAll(data.msg);
            } else if (response.status !== 404) { // probably some other client side error 
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else { // else 404 not found 
                $("#status").text("no such path on server");
            } // else 
        } catch (error) {
            $("#status").text(error.message);
        }  // try/catch 
        $("#theModal").modal("toggle");
    }; // add


    $("#actionbutton").on("click", () => {
        $("#actionbutton").val() === "update" ? update() : add();
    }); // actionbutton click


    const _delete = async () => {
        let student = JSON.parse(sessionStorage.getItem("student"));
        try {
            $("#dialog").show(); // Show the confirmation dialog
            $("#modalstatus").text("update data");
            // Attach event handlers
            $("#yesbutton").off("click").on("click", async () => {
                try {
                    let response = await fetch(`/api/student/${student.id}`, {
                        method: 'DELETE',
                        headers: { 'Content-Type': 'application/json; charset=utf-8' }
                    });

                    if (response.ok) {
                        let data = await response.json();
                        getAll(data.msg); // Refresh the student list
                        $('#modalstatus').text('Student deleted successfully!');
                    } else {
                        $('#status').text(`Status - ${response.status}, Problem on delete server side, see server console`);
                    }
                    $('#theModal').modal('toggle'); // Close the modal
                } catch (error) {
                    $('#status').text(error.message);
                } finally {
                    $("#dialog").hide(); // Always hide the dialog
                }
            });

            $("#nobutton").off("click").on("click", () => {
                $('#modalstatus').text('Delete cancelled!');
                $("#dialog").hide(); // Hide the dialog if the user cancels
            });

        } catch (error) {
            $('#status').text(error.message);
        }
    }; //_delete


    $("#deletebutton").on("click", () => {
        _delete();
    }); // deletebutton click


    const loadMajorDDL = (stumjr) => {
        html = '';
        $('#ddlMajors').empty();
        let allmajors = JSON.parse(sessionStorage.getItem('allmajors'));
        allmajors.forEach((mjr) => { html += `<option value="${mjr.id}">${mjr.majorName}</option>` });
        $('#ddlMajors').append(html);
        $('#ddlMajors').val(stumjr);
    }; // loadMajorDDL


    


}); // jQuery ready method