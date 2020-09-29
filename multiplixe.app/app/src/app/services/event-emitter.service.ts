import { EventEmitter } from '@angular/core';

import { BaseService } from './base.service';

export class EventEmitterService extends BaseService {

   private emitters: { [nomeEvento: string]: EventEmitter<any> } = {}

   get(nomeEvento: string): EventEmitter<any> {
      if (!this.emitters[nomeEvento])
         this.emitters[nomeEvento] = new EventEmitter<any>();
      return this.emitters[nomeEvento];
   }
}