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