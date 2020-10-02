let getAllBtn = document.getElementById("btn1");
let getByIdBtn = document.getElementById("btn2");
let addUserNameBtn = document.getElementById("btn3");
let DeleteUserNameBtn = document.getElementById("btn4");
let getByIdInput = document.getElementById("input2");
let addUserInput = document.getElementById("input32");
let DeleteUserInput = document.getElementById("input4");
////////////////////////////////////////////////////////
let getAllUsersBtn = document.getElementById("btn5");
let addNewUserBtn = document.getElementById("btn6");
let idInput = document.getElementById("input5");
let firstNameInput = document.getElementById("input6");
let lastNameInput = document.getElementById("input7");


let port = "14906";
let getAllUsernames = async () => {
    let url = "http://localhost:" + port + "/api/users";

    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
};

let getUserById = async () => {
    let url = "http://localhost:" + port + "/api/users/" + getByIdInput.value;

    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
};

let addUserName = async () => {
    let url = "http://localhost:" + port + "/api/users";
    var response = await fetch(url, {
       method: 'POST',
       headers: {
           'Content-Type': 'application/json'
       },
        body: addUserInput.value
    })
        var data = await response.text();
        console.log(data);
    console.log(addUserInput.value);
}

let deleteUserName = async () => {
    let url = "http://localhost:" + port + "/api/users";
    var response = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: DeleteUserInput.value
    });
    var data = await response.text();
    console.log(data);
    console.log(response);
}

////////////////////////////////////////////////////////

let getAllUsers = async () => {
    let url = "http://localhost:" + port + "/api/user";

    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
};


let addNewUser = async () => {
    let url = "http://localhost:" + port + "/api/user";

    let user = { id: idInput.value, firstname: firstNameInput.value, lastname: lastNameInput.value }
    var response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    });
    console.log(response);
}

getAllBtn.addEventListener("click", getAllUsernames);
getByIdBtn.addEventListener("click", getUserById);
addUserNameBtn.addEventListener("click", addUserName);
DeleteUserNameBtn.addEventListener("click", deleteUserName);
////////////////////////////////////////////////////////
getAllUsersBtn.addEventListener("click", getAllUsers);
addNewUserBtn.addEventListener("click", addNewUser);
