// create secrets.js with real data
// const firebaseConfig = {
//     apiKey: "apiKey",
//     authDomain: "authDomain",
//     projectId: "projectId",
//     storageBucket: "storageBucket",
//     messagingSenderId: "messagingSenderId",
//     appId: "appId"
// };

firebase.initializeApp(firebaseConfig);

const db = firebase.firestore();
const usersRef = db.collection('users');
const taskListsRef = db.collection('taskLists');
const tasksRef = db.collection('tasks');

async function getUsers() {
    return await usersRef.get()
}

async function getUser(id) {
    var querySnapshot = await usersRef.where("__name__", "==", id).get()    
    return querySnapshot.docs
        .map(x => new User(
            querySnapshot.docs[0].id,
            x.data().login)
        )[0]
}

async function getTaskLists() {
    return await taskListsRef.get()
}

async function getTaskList(id) {
    var querySnapshot = await taskListsRef.where("__name__", "==", id).get()
    return querySnapshot.docs
        .map(x => new TaskList(
            querySnapshot.docs[0].id,
            x.data().title,
            x.data().userId,
            x.data().creationTime,
            x.data().completionTime,
        ))[0]
}

async function getTasksOfTaskList(taskListId) {

    var querySnapshot = await tasksRef
        .where("taskListId", "==", taskListId)
        .get()
    
    return querySnapshot.docs
        .map(x => new Task(
            querySnapshot.docs[0].id,
            x.data().title,
            x.data().completed,
            x.data().taskListId,
            x.data().creationTime,
            x.data().completionTime,
        ))
    .sort((a, b) => b.creationTime - a.creationTime)
}

async function addTask(task) {
    console.log(task);
    return await tasksRef.add({
        title: task.title,
        taskListId: task.taskListId,
        completed: task.completed,
        creationTime: new Date(),
        completionTime: null
      });
}

// const usersDocuments  = await getUsers()
// usersDocuments.forEach(doc => {
//     console.log(doc.id, doc.data())
// })

// const taskListsDocuments = await getTaskLists()
// taskListsDocuments.forEach(doc => {
//     console.log(doc.id, doc.data())
// })