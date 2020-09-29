export class Placing {

   public user: string = '';
   public name: string = '';
   public nickname: string = '';
   public points: number = 0;
   public value: number = 0;
   public imageUrl: string = '';

   static create() {
      return new Placing();
   }
}