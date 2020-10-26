import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class LoaderService extends BaseService {

  constructor() {
    super()
  }

  async run(text: string = null) {
    console.log("Iniciar Loader");
  }

  async stop() {
    console.log("Finalizar Loader");
  }
}
