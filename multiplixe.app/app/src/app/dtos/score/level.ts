export class Level {
   public id: number = 0;
   public levelChange: boolean = false;
   public name: string = '';

   static create() {
      return new Level();
   }

}