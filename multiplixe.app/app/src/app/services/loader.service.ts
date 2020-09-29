import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { AppInjector } from '../app-injector';
import { EventEmitterService } from './event-emitter.service';

@Injectable({
  providedIn: 'root'
})
export class LoaderService extends BaseService {

  protected eventEmitter: EventEmitterService;

  constructor() {
    super()
    this.eventEmitter = AppInjector.get(EventEmitterService)
  }

  async run(text: string = null) {
    this.eventEmitter.get('loader').emit({ show: true, text: text });
  }

  async stop() {
    this.eventEmitter.get('loader').emit({ show: false });
  }
}
