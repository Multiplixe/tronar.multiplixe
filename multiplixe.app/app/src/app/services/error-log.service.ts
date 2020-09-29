import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ToastController } from '@ionic/angular';
import { AppInjector } from '../app-injector';

@Injectable({
  providedIn: 'root'
})
export class ErrorLogService extends BaseService {

  constructor() {
    super()
  }

  async log(error: any) {
    console.log("TODO logar na api")
    console.log(error);
  }

}
