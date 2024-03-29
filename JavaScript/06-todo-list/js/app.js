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

let draggableElement

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

listNameEditIcon.addEventListener('click', async () => {
    
    const listNameEditDiv = document.querySelector('.listNameEdit')
    const listNameEditInput = document.querySelector('.listNameEditInput')
    console.log(listNameEditDiv);

    if (listNameEditDiv.style.display == 'block') {
        const newTaskListName = listNameEditInput.value
        listNameEditInput.value = ''
        taskListName.style.display = 'block'
        listNameEditDiv.style.display = 'none'
        listNameEditIcon.innerHTML = 'edit'

        if (newTaskListName != currentTaskList.title) {
            currentTaskList.title = newTaskListName
            updateTaskListName()
            await updateTaskList(currentTaskList)
            console.log('task list updated');
        }
    } else {
        listNameEditInput.value = currentTaskList.title
        taskListName.style.display = 'none'
        listNameEditDiv.style.display = 'block'
        listNameEditIcon.innerHTML = 'done'
    }
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

    updateTaskListName()
    placeEventsOnPlaceholders()
    setLoadingView(false)
}

function updateTaskListName() {
    taskListName.innerHTML = `${currentTaskList.title} (${currentUser.login})`
}

function setLoadingView(isLoading) {

    loading.style.display = isLoading ? 'block' : 'none';
    newTask.style.display = isLoading ? 'none' : 'flex'
    listName.style.display = isLoading ? 'none' : 'flex'
}

function generateTask(task, addToBegin = true) {

    // const placeholderId = addToBegin ? 0 :
    // const dragPlaceholderHtml = `<div class="placeholder" id=""></div>`
    const dragPlaceholderHtml = ''
    
    taskList.insertAdjacentHTML(
        addToBegin ? 'afterbegin' : 'beforeend',
        `${addToBegin ? dragPlaceholderHtml : ''}
        <div draggable="true" class="task${task.completed ? ' completed' : ''}">
            <input type="hidden" id="id" value="${task.id}">
            <div class="drag">
                <span class="material-icons md-20">drag_indicator</span>
            </div>
            <div class="color"></div>
            <div class="task-edit">
                <span id="task-edit-icon" class="material-icons md-18">edit</span>
            </div>
            <div class="check">
                <input type="checkbox" class='chk-input'>
            </div>

            <div class="task-name-edit">
                <input class="task-name-edit-input" type="text">
            </div>

            <span class="task-text">${task.title}</span>
            
            <div class="delete-forever">
                <span class="material-icons md-20">delete_forever</span>
            </div>        
        </div>
        ${!addToBegin ? dragPlaceholderHtml : ''}`
    );

    const taskNodes = taskList.querySelectorAll('.task');

    const newTask = taskNodes[addToBegin ? 0 : taskNodes.length - 1];
    const newCheckbox = newTask.querySelector('.chk-input')
    const newTaskEditDiv = newTask.querySelector('.task-edit')
    const newTaskEditIcon = newTask.querySelector('#task-edit-icon')
    const newTaskEditNameDiv = newTask.querySelector('.task-name-edit')
    const newTaskEditNameInput = newTask.querySelector('.task-name-edit-input')
    const newText = newTask.querySelector('.task-text')
    const newDeleteForever = newTask.querySelector('.delete-forever')

    newCheckbox.checked = task.completed

    placeEventOnTaskBlock(newTask)

    // OMG: refactor me please!!!
    placeEventOnEditTaskName(newTaskEditDiv, newTaskEditIcon, newTaskEditNameDiv,
        newTaskEditNameInput, newText, newCheckbox, task.id)
    placeEventsOnCompleteTask(newText, newCheckbox, newTask, task.id)
    placeEventOnDeleteForever(newDeleteForever, task.id)
}

function placeEventOnTaskBlock(task) {

    task.addEventListener('mouseover', () => {
        task.querySelector('.delete-forever').style.display = 'flex'
    })

    task.addEventListener('mouseout', () => {
        task.querySelector('.delete-forever').style.display = 'none'
    })

    task.addEventListener('dragstart', () => {
        task.classList.add('hold')
        draggableElement = task
        //setTimeout(() => task.classList.add('hide'))
    })

    task.addEventListener('dragend', () => {
        task.classList.remove('hold')
        task.classList.remove('hide')

        draggableElement = undefined
    })
}

function placeEventOnEditTaskName(taskEditIcon, newTaskEditIcon, editDiv,
    input, newText, newCheckbox, taskId) {

    taskEditIcon.addEventListener('click', async () => {

        const taskIndex = arrTasks
            .indexOf(arrTasks.find(x => x.id === taskId))

        if (editDiv.style.display == 'block') {

            newText.innerHTML = input.value
            if (arrTasks[taskIndex].title != input.value) {
                arrTasks[taskIndex].title = input.value
                await updateTask(arrTasks[taskIndex])
            }

            newText.style.display = 'block'
            newCheckbox.style.display = 'block'
            editDiv.style.display = 'none'
            newTaskEditIcon.innerHTML = 'edit'
        } else {

            input.value = arrTasks[taskIndex].title
            newText.style.display = 'none'
            newCheckbox.style.display = 'none'
            editDiv.style.display = 'block'
            newTaskEditIcon.innerHTML = 'done'
        }
    })
}

function placeEventsOnCompleteTask(textInput, chkInput, taskDiv, taskId) {

    chkInput.addEventListener('change', async (event) => {

        const taskDiv = chkInput.parentElement.parentElement
        await completeTask(taskDiv, event.currentTarget.checked, taskId)
    })

    textInput.addEventListener('click', async () => {
        var chkInput = taskDiv.querySelector('.chk-input')
        await completeTask(taskDiv, !chkInput.checked, taskId)
    })
}

function placeEventsOnPlaceholders() {
    const placeholders = document.querySelectorAll('.placeholder')

    for (const placeholder of placeholders) {

        placeholder.addEventListener('dragover', (event) => {
            event.preventDefault()
        })

        placeholder.addEventListener('dragenter', (event) => {
            placeholder.classList.add('hovered')
        })

        placeholder.addEventListener('dragleave', (event) => {
            placeholder.classList.remove('hovered')
        })

        placeholder.addEventListener('drop', (event) => {
            placeholder.classList.remove('hovered')
            placeholder.append(draggableElement)
        })
    }
}

async function completeTask(taskDiv, completed, taskId) {

    var chkInput = taskDiv.querySelector('.chk-input')

    if (completed) {
        taskDiv.classList.add('completed')
        chkInput.checked = true
    } else {
        taskDiv.classList.remove('completed')
        chkInput.checked = false
    }

    const taskIndex = arrTasks
        .indexOf(arrTasks.find(x => x.id === taskId))

    arrTasks[taskIndex].completed = completed
    arrTasks[taskIndex].completionTime = completed
        ? new Date() : null;
    await updateTask(arrTasks[taskIndex])
}

function placeEventOnDeleteForever(input, taskId) {

    input.addEventListener('click', async () => {
        input.parentElement.remove()
        await deleteTask(taskId)
    })
}