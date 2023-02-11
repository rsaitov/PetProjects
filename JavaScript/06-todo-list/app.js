const dbFilePath = 'db.txt'
const apiUrl = 'https://jsonplaceholder.typicode.com/todos'

const listName = document.querySelector('.listname')
const listNameEditIcon = document.querySelector('#listNameEditIcon')
const container = document.querySelector('.container')
const taskList = document.querySelector('.tasklist')
const newTaskText = document.querySelector('#newtasktext')
const addButton = document.querySelector('#addBtn')

let arrTasks = [];

listName.addEventListener('mouseover', () => {
    listNameEditIcon.style.display = "block";
})

listName.addEventListener('mouseout', () => {
    listNameEditIcon.style.display = "none";
})

listNameEditIcon.addEventListener('click', () => {
    console.log('edit')
})

addButton.addEventListener('click', () => {
    if (newTaskText.value.trim() === '')
        return
    
    const newTask = new Task(null, null, newTaskText.value.trim(), false)

    generateTask(newTask)

    newTaskText.value = ''
})

getData()

function placeEventsOnCheckbox(chkInput) {

    chkInput.addEventListener('change', (event) => {
        const parent = chkInput.parentElement.parentElement
        if (event.currentTarget.checked) {
            parent.classList.add('completed')
        } else {
            parent.classList.remove('completed')
        }
    })
}

function placeEventsOnTaskText(textInput) {

    textInput.addEventListener('click', (event) => {
        const parent = textInput.parentElement

        var chkInput = parent.querySelector('.chk-input')
        chkInput.checked = !chkInput.checked;

        if (chkInput.checked) {
            parent.classList.add('completed')
        } else {
            parent.classList.remove('completed')
        }
    })
}

function getData() {
    loadFromApi()
    // readTasksFromFile(dbFilePath)
}

function loadFromApi() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(json => {
            json.forEach(x => { 
                const task = new Task(x.userId, x.id, x.title, x.completed)
                arrTasks.push(task)
                generateTask(task, false)
            })
        })
}

function readTasksFromFile(file) {

    fetch(file)
        .then(response => response.text())
        .then(text => {
            arrTasks = text.split('\r\n');
            arrTasks.forEach(x => {
                const args = x.split(';')
                if (args[0].trim() != 0) {
                    generateTask(args[0], args[1], false)
                }
            })
        })
}

function generateTask(task, addToBegin = true) {

    taskList.insertAdjacentHTML(
        addToBegin ? 'afterbegin' : 'beforeend',
        `<div class="task${task.completed ? ' completed' : ''}">
        <div class="drag">
            <span class="material-icons md-20">drag_indicator</span>
        </div>
        <div class="color"></div>
        <div class="check">
            <input type="checkbox" class='chk-input'>
        </div>
        <span class="task-text">${task.title}</span>
    </div>`);

    var checkboxNodes = taskList.querySelectorAll('.chk-input');
    let newCheckbox = checkboxNodes[addToBegin ? 0 : checkboxNodes.length - 1];
    newCheckbox.checked = task.completed == 1;

    const textsNodes = document.querySelectorAll('.task-text')
    let newText = textsNodes[addToBegin ? 0 : textsNodes.length - 1];

    placeEventsOnCheckbox(newCheckbox);
    placeEventsOnTaskText(newText);
}

class Task {
    constructor(userId, id, title, completed) {
        this.userId = userId;
        this.id = id;
        this.title = title;
        this.completed = completed;
    }
}