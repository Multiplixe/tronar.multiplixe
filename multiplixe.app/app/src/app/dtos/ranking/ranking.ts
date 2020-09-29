import { Placing } from './placing';

export class Ranking {
   public date: Date;
   public placing: Placing[] = [];

   public value: number = 0;

   static create() {
      return new Ranking();
   }

}