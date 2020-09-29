import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class DataTransferService extends BaseService {

  private data: any = {};

  constructor() {
    super();
  }

  set(d:any) {
    this.data = d;
  }

  get() {
    let data = this.data;
    this.data = undefined;
    return data;
  }
}
