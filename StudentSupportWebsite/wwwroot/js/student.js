$(() => {  

    $("#getbutton").on('click', async (e) => {  
        try {
            let email = $("#TextBoxEmail").val();
            $("#status").text("please wait...");
            let response = await fetch(`/api/student/${email}`);
            if (response.ok) {
                let data = await response.json();
                if (data.email !== "not found") {
                    $("#title").text(data.title);
                    $("#firstname").text(data.firstname);
                    $("#lastname").text(data.lastname);
                    $("#phoneno").text(data.phoneno);
                    $("#status").text("student found");
                } else {
                    $("#title").text("not found");
                    $("#firstname").text("not found");
                    $("#lastname").text("not found");
                    $("#phoneno").text("not found");
                    $("#status").text("no such student");
                }
            } else if (response.status !== 404) {
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else {
                $("#status").text("no such path on server");
            } // else 
        } catch (error) {
            $("#status").text(error.message);
        }  // try/catch 

    }); // click event


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

}); // main jQuery method 

// server was reached but server had a problem with the call 
const errorRtn = (problemJson, status) => {
    if (status > 499) {
        $("#status").text("Problem server side, see debug console");
    } else {
        let keys = Object.keys(problemJson.errors)
        problem = {
            status: status,
            statusText: problemJson.errors[keys[0]][0],
        };
        $("#status").text("Problem client side, see browser console");
        console.log(problem);
    } // else 
} 