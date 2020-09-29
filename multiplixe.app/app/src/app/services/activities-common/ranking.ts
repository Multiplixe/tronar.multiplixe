import { IActivity } from './iactivity';
import { CacheService } from '../cache.service';
import { AppInjector } from 'src/app/app-injector';
import { EventEmitterService } from '../event-emitter.service';

export class Ranking implements IActivity {

   private cacheService: CacheService;
   protected eventEmitter: EventEmitterService;
   
   constructor() {
      this.cacheService = AppInjector.get(CacheService);
      this.eventEmitter = AppInjector.get(EventEmitterService);
   }

   execute() {
      this.cacheService.setUpdatedRanking(false);
      this.eventEmitter.get('ranking-updated').emit();
   }

   static create() {
      return new Ranking();
   }
}