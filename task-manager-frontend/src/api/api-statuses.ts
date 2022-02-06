import { HttpClient, HttpHeaders, HttpXhrBackend } from '@angular/common/http';

const http:HttpClient = new HttpClient(new HttpXhrBackend({ build: () => new XMLHttpRequest() }));

export function GetStatuses(username:string,accessKey:string){
    const headers= new HttpHeaders()
    .set('username', username)
    .set('accessKey', accessKey);
    return http.get('https://localhost:7135/api/statuses', {responseType:"text",'headers': headers}).toPromise().catch(e => console.log(e));
}