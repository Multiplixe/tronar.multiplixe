import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EventEmitterService {

  private emitters: { [nomeEvento: string]: EventEmitter<any> } = {}
  constructor() { }


  get(eventName: string): EventEmitter<any> {
    if (!this.emitters[eventName])
       this.emitters[eventName] = new EventEmitter<any>();
    return this.emitters[eventName];
 }

}
