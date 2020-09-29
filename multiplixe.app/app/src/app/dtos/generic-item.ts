export class GenericItem {
   public id : number = 0;
   public name : string = '';

   static create() {
      return new GenericItem();
   }
}