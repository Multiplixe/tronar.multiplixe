import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ToastController } from '@ionic/angular';
import { AppInjector } from '../app-injector';

@Injectable({
  providedIn: 'root'
})
export class ToastService extends BaseService {

  public toastController: ToastController;

  constructor() {
    super()
    this.toastController = AppInjector.get(ToastController)
  }

  async show(text: string) {
    const toast = await this.toastController.create({
      message: text,
      duration: 3000
    });
    toast.present();
  }

}
