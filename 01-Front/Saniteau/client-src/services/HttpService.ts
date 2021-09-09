import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class HttpService {
    constructor(@Inject(HttpClient) private httpClient: HttpClient) { }

    public postAsObservable(url: string, data: any) {
        let bodyContent = '';
        if (data != null) {
            bodyContent = JSON.stringify(data);
        }
        let authToken = localStorage.getItem('auth_token');
        let headers = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set('Authorization', `Bearer ${authToken}`);

        let options = {
            headers: headers
        }
        //this.httpClient.post(url, bodyContent, options)
        //    .subscribe(data => {
        //        alert(data);
        //        alert(JSON.stringify(data));
        //    });
        return this.httpClient.post(url, bodyContent, options);
    }

    public getAsObservable(url: string) {
        let authToken = localStorage.getItem('auth_token');
        let headers = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set('Authorization', `Bearer ${authToken}`);
        let options = {
            headers: headers
        }
        //this.httpClient.post(url, bodyContent, options)
        //    .subscribe(data => {
        //        alert(data);
        //        alert(JSON.stringify(data));
        //    });
        return this.httpClient.get(url, options);
    }
}