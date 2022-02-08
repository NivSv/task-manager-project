import { HttpClient, HttpHeaders, HttpXhrBackend } from '@angular/common/http';

const http:HttpClient = new HttpClient(new HttpXhrBackend({ build: () => new XMLHttpRequest() }));

export function GetTasks(username:string,accessKey:string){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/tasks', {responseType:"json",'headers': headers});
}

export function GetTasksByDeadline(username:string,accessKey:string,date:string){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/tasks/deadline/'+date, {responseType:"text",'headers': headers});
}

export function GetTasksByStatus(username:string,accessKey:string,status:string){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/tasks/status/'+status, {responseType:"text",'headers': headers});
}

export function GetTasksByPriority(username:string,accessKey:string,priority:string){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/tasks/priority/'+priority, {responseType:"text",'headers': headers});
}

export function GetTasksByID(username:string,accessKey:string,id:number){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/tasks/'+id, {responseType:"text",'headers': headers});
}

export function DeleteTask(username:string,accessKey:string,id:number){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey)
    .set('Content-Type', 'application/json');
    return http.delete('https://localhost:7135/api/tasks/'+id,{responseType:"text",'headers': headers});
}

export function CreateTask(username:string,accessKey:string,taskTitle:string,taskDescription:string,taskPriority:string,taskDeadline:string,assignee:string){
    var body = '{"taskTitle": "'+taskTitle+'", "taskDescription": "'+taskDescription+'", "taskPriority": "'+taskPriority+'", "taskDeadline": "'+taskDeadline+'", "taskStatus": "Pending", "assignee": "'+assignee+'"}';
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey)
    .set('Content-Type', 'application/json');
    return http.post('https://localhost:7135/api/tasks',body,{responseType:"text",'headers': headers});
}

export function EditTask(username:string,accessKey:string,taskID:number,taskTitle:string,taskDescription:string,taskPriority:string,taskDeadline:string,taskStatus:string,assignee:string){
    var body = '{"taskTitle": "'+taskTitle+'", "taskDescription": "'+taskDescription+'", "taskPriority": "'+taskPriority+'", "taskDeadline": "'+taskDeadline+'", "taskStatus": "'+taskStatus+'", "assignee": "'+assignee+'"}';
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey)
    .set('Content-Type', 'application/json');
    return http.put('https://localhost:7135/api/tasks/'+taskID,body,{responseType:"text",'headers': headers});
}