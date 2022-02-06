import { HttpClient, HttpHeaders, HttpXhrBackend } from '@angular/common/http';

const http:HttpClient = new HttpClient(new HttpXhrBackend({ build: () => new XMLHttpRequest() }));


export function Register(username:string, password:string){
    var body = '{"username": "'+username+'", "password": "'+password+'"}';
    return http.post('https://localhost:7135/api/users/register',body,{responseType:"text"}).toPromise().catch(e => console.log(e));
}

export function Login(username:string, password:string){
    var body = '{"username": "'+username+'", "password": "'+password+'"}';
    return http.post('https://localhost:7135/api/users/login',body,{responseType:"text"}).toPromise().catch(e => console.log(e));
}

export async function GetUsers(username:string,accessKey:string){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/users', {responseType:"text",'headers': headers}).toPromise().catch(e => console.log(e));
}

export function GetUser(username:string,accessKey:string){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/users/'+username, {responseType:"text",'headers': headers}).toPromise().catch(e => console.log(e));
}