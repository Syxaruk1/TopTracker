import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cutText',
})
export class CutTextPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    if(value.length > 22) return value.substring(0,22) + "...";
    return value;
  }

}
