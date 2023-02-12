const dbFilePath = 'db.txt'

const listName = document.querySelector('.listname')
const taskListName = document.querySelector('#taskListName')
const listNameEditIcon = document.querySelector('#listNameEditIcon')
const container = document.querySelector('.container')
const loading = document.querySelector('#loading')
const newTask = document.querySelector('.newtask')
const taskList = document.querySelector('.tasklist')
const newTaskText = document.querySelector('#newtasktext')
const addButton = document.querySelector('#addBtn')

let currentUserId = 'iTQtInegvrfZJ6ba4AvG'
let currentTaskListId = 'STbCaMfARbICbnNyJVqD'

let currentUser
let currentTaskList

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

addButton.addEventListener('click', async () => {
    
    if (newTaskText.value.trim() === '') {
        return
    }

    const newTask = new Task(null, newTaskText.value.trim(), false, currentTaskList.id)
    newTaskText.value = ''
    await addTask(newTask)

    generateTask(newTask)
})

loadDataAndFullControls()

async function loadDataAndFullControls() {

    setLoadingView(true)

    newTask.style.display = 'none'
    listName.style.display = 'none'

    currentUser = await getUser(currentUserId)
    currentTaskList = await getTaskList(currentTaskListId)

    await loadTasksFromFirestore(currentTaskList.id)
}

async function loadTasksFromFirestore(taskListId) {

    const tasks = await getTasksOfTaskList(taskListId)
    tasks.forEach(task => {
        arrTasks.push(task)
        generateTask(task, false)
    })

    taskListName.innerHTML = `${currentTaskList.title} (${currentUser.login})`
    setLoadingView(false)
}

function setLoadingView(isLoading) {

    loading.style.display = isLoading ? 'block' : 'none';
    newTask.style.display = isLoading ? 'none' : 'flex'
    listName.style.display = isLoading ? 'none' : 'flex'
}

function generateTask(task, addToBegin = true) {

    taskList.insertAdjacentHTML(
        addToBegin ? 'afterbegin' : 'beforeend',
        `<div class="task${task.completed ? ' completed' : ''}">
            <input type="hidden" id="id" value="${task.id}">
            <div class="drag">
                <span class="material-icons md-20">drag_indicator</span>
            </div>
            <div class="color"></div>
            <div class="check">
                <input type="checkbox" class='chk-input'>
            </div>
            <span class="task-text">${task.title}</span>
            <div class="delete-forever"><span class="material-icons md-20">delete_forever</span></div>        
        </div>`);

    const taskNodes = taskList.querySelectorAll('.task');

    const newTask = taskNodes[addToBegin ? 0 : taskNodes.length - 1];
    const newCheckbox = newTask.querySelector('.chk-input')
    const newText = newTask.querySelector('.task-text')
    const newDeleteForever = newTask.querySelector('.delete-forever')

    placeEventOnTaskBlock(newTask)
    placeEventOnCheckbox(newCheckbox)
    placeEventOnTaskText(newText)
    placeEventOnDeleteForever(newDeleteForever, task.id)
}

function placeEventOnTaskBlock(task) {

    task.addEventListener('mouseover', () => {
        task.querySelector('.delete-forever').style.display = 'flex'
    })

    task.addEventListener('mouseout', () => {
        task.querySelector('.delete-forever').style.display = 'none'
    })
}

function placeEventOnCheckbox(chkInput) {

    chkInput.addEventListener('change', (event) => {

        const parent = chkInput.parentElement.parentElement
        if (event.currentTarget.checked) {
            parent.classList.add('completed')
        } else {
            parent.classList.remove('completed')
        }
    })
}

function placeEventOnTaskText(textInput) {

    textInput.addEventListener('click', () => {

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

function placeEventOnDeleteForever(input, taskId) {

    input.addEventListener('click', async () => {
        input.parentElement.remove()
        await deleteTask(taskId)
    })
}