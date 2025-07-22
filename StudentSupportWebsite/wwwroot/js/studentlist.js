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
        } catch (error) {
            $("#status").text(error.message);
        }
    }; // getAll     


    const buildStudentList = (data) => {
        $("#studentList").empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Student Info</div> 
                  <div class= "list-group-item row d-flex text-center" id="heading"> 
                  <div class="col-4 h4">Title</div> 
                  <div class="col-4 h4">First</div> 
                  <div class="col-4 h4">Last</div> 
               </div>`);
        div.appendTo($("#studentList"));
        sessionStorage.setItem("allstudents", JSON.stringify(data));
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
            data.forEach(student => {
                if (student.id === parseInt(id)) {
                    $("#TextBoxTitle").val(student.title);
                    $("#TextBoxFirstName").val(student.firstname);
                    $("#TextBoxLastName").val(student.lastname);
                    $("#TextBoxEmail").val(student.email);
                    $("#TextBoxPhoneNo").val(student.phoneno);
                    sessionStorage.setItem("student", JSON.stringify(student));
                    $("#modalstatus").text("update data");
                    $("#theModal").modal("toggle");
                } // if 
            }); // data.map 
        } else {
            return false; // ignore if they clicked on heading or status 
        }
    }); // studentListClick


    $("#actionbutton").on('click', async (e) => {
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
    }); // action button click 

}); // jQuery ready method