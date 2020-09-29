import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Dashboard } from '../dtos/dashboard';
import { Ranking } from '../dtos/ranking/ranking';

@Injectable({
  providedIn: 'root'
})
export class CacheService extends BaseService {

  private _cache = 'cache';

  constructor() {
    super();
  }

  setUpdatedRanking(updated: boolean) {
    this.localStorageService.setItem(this.key('ranking-updated'), updated);
  }

  getUpdatedRanking() {
    return <boolean>this.localStorageService.get(this.key('ranking-updated'));
  }  

  setUserRanking(position: number) {
    this.localStorageService.setItem(this.key('ranking-user'), position);
    this.setUpdatedRanking(true);
  }

  getUserRanking(): number {
    return this.get<number>('ranking-user');
  }

  setRanking(dto: Ranking) {
    this.localStorageService.setItem(this.key('ranking'), dto);
    this.setUpdatedRanking(true);
  }

  getRanking(): Ranking {
    return this.get<Ranking>('ranking');
  }

  setDashboard(dto: Dashboard) {
    this.localStorageService.setItem(this.key('dashboard'), dto);
  }

  getDashboard(): Dashboard {
    return this.get<Dashboard>('dashboard');
  }

  private get<T>(key:string) {
    return <T>this.localStorageService.get(this.key(key));
  }

  private key(_key) {
    return `${this._cache}:${_key}`;
  }

}
