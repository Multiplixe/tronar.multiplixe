import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';

@Injectable({
  providedIn: 'root'
})
export class UploadService extends BaseService {

  private httpService: HttpService;

  constructor() {
    super();
    this.httpService = AppInjector.get(HttpService)
  }

  avatar(d: FormData) {
    return this.httpService.upload("restrito/upload/avatar", d)
  }

}
