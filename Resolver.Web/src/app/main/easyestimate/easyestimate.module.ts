import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { EasyestimateComponent } from './easyestimate.component';

const routes = [
    {
        path     : 'easyestimate',
        component: EasyestimateComponent
    }
];

@NgModule({
    declarations: [
        EasyestimateComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        EasyestimateComponent
    ]
})

export class EasyestimateModule
{
}
