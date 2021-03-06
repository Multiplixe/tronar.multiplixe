import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from "@angular/common/http";

import { AppInjector } from '../app-injector';
import { Envelope } from '../envelopes/envelope';
import { apiEnvironment } from '../../environments/environment';

@Injectable({
  providedIn: "root"
})
export class HttpService {

  private _http: HttpClient;

  constructor() {
    this._http = AppInjector.get(HttpClient);
  }

  post<T>(url: string, o: any, headers: HttpHeaders) {
    return this._http.post<T>(url, o, { headers: headers }).toPromise();
  }

  async get<T>(url: string, headers: HttpHeaders) {
    return this._http.get<T>(url, { headers: headers }).toPromise();
  }

  postAPI<T>(url: string, o: any): Promise<Envelope<T>> {

    let request: Envelope<T> = this._createRequest(o);

    return new Promise((resolve, reject) => {
      this._http.post<Envelope<T>>(apiEnvironment.host + url, request,
        {
          observe: "response",
          headers: new HttpHeaders(
            {
              'Content-Type': 'application/json',
              'Access-Control-Allow-Origin': '*'
            }
          )
        })
        .toPromise()
        .then((r) => { resolve(r.body); })
        .catch(e => { reject(e); })
        .finally(() => {
        });
    });

  }

  putAPI<T>(url: string, o: any): Promise<Envelope<T>> {

    let request: Envelope<T> = this._createRequest(o);

    return new Promise((resolve, reject) => {
      this._http.put<Envelope<T>>(apiEnvironment.host + url, request, { observe: "response" })
        .toPromise()
        .then((r) => { resolve(r.body); })
        .catch(e => { reject(e); })
        .finally(() => {
        });
    });

  }

  getAPI<T>(url: string): Promise<Envelope<T>> {

    return new Promise((resolve, reject) => {
      this._http.get<Envelope<T>>(apiEnvironment.host + url, { observe: "response" })
        .toPromise()
        .then((r) => { resolve(r.body); })
        .catch(e => { reject(e); })
        .finally(() => {
        });
    });
  }

  deleteAPI(url: string): Promise<HttpResponse<any>> {

    return new Promise((resolve, reject) => {
      this._http.delete<any>(apiEnvironment.host + url, { observe: "response" })
        .toPromise()
        .then((r) => { resolve(r.body); })
        .catch(e => { reject(e); })
        .finally(() => {
        });
    });
  }

  private _createRequest(o: any): Envelope<any> {
    let request = new Envelope<any>();
    request.item = o;
    return request;
  }



}
