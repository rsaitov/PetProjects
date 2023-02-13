class User {
    constructor(id, login) {
        this.id = id
        this.login = login
    }
}

class TaskList {
    constructor(id, title, userId, creationTime, completionTime) {
        this.id = id
        this.title = title
        this.userId = userId
        this.creationTime = creationTime
        this.completionTime = completionTime
    }
}

class Task {
    constructor(
        id,
        title,
        completed,
        taskListId,
        creationTime,
        completionTime,
        // orderNumber,
    ) {
        this.id = id
        this.title = title
        this.completed = completed
        this.taskListId = taskListId
        this.creationTime = creationTime
        this.completionTime = completionTime
        // this.orderNumber = orderNumber
    }
}