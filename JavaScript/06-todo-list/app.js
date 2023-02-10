const dbFilePath = 'db.txt'

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

    generateTask(newTaskText.value.trim())
    placeEventsOnCheckboxes();
    placeEventsOnTaskTexts();

    newTaskText.value = ''
})

readTasksFromFile(dbFilePath);

function placeEventsOnCheckboxes() {
    const checkboxes = document.querySelectorAll('.chk-input')

    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', (event) => {
            const parent = checkbox.parentElement.parentElement
            if (event.currentTarget.checked) {
                parent.classList.add('completed')
            } else {
                parent.classList.remove('completed')
            }
        })
    });
}

function placeEventsOnTaskTexts() {
    const taskTexts = document.querySelectorAll('.task-text')

    taskTexts.forEach(taskText => {
        taskText.addEventListener('click', (event) => {

            const parent = taskText.parentElement
            parent.classList.add('completed')

            var chkInput = parent.querySelector('.chk-input')
            chkInput.checked = !chkInput.checked;

            if (event.currentTarget.checked) {
            } else {
                parent.classList.remove('completed')
            }
        })
    });
}

function readTasksFromFile(file) {

    fetch(file)
        .then(response => response.text())
        .then(text => {
            arrTasks = text.split('\r\n');
            arrTasks.forEach(x => {
                const args = x.split(';')
                generateTask(args[0], args[1], false)
            })

            placeEventsOnCheckboxes();
            placeEventsOnTaskTexts();
        })
}

function generateTask(text, completed, addToBegin = true) {
    taskList.insertAdjacentHTML(
        addToBegin ? 'afterbegin' : 'beforeend',
        `<div class="task${completed == 1 ? ' completed' : ''}">
        <div class="drag">
            <span class="material-icons md-20">drag_indicator</span>
        </div>
        <div class="color"></div>
        <div class="check">
            <input type="checkbox" class='chk-input'>
        </div>
        <span class="task-text">${text}</span>
    </div>`);
}