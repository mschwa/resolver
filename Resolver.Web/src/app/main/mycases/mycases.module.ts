import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { MycasesComponent } from './mycases.component';

const routes = [
    {
        path     : 'mycases',
        component: MycasesComponent
    }
];

@NgModule({
    declarations: [
        MycasesComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        MycasesComponent
    ]
})

export class MycasesModule
{
}
