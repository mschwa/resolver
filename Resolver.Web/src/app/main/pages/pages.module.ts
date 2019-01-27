import { NgModule } from '@angular/core';

import { RegisterModule } from 'app/main/pages/account/register/register.module';
import { SampleModule } from '../sample/sample.module';
import { QuestionsModule } from 'app/main/questions/questions.module';
import { EasyestimateModule } from 'app/main/easyestimate/easyestimate.module';
import { RefercaseModule } from 'app/main/refercase/refercase.module';
import { SettingsModule } from 'app/main/settings/settings.module';
import { MycasesModule } from 'app/main/mycases/mycases.module';
import { LoginModule } from './account/login/login.module';

@NgModule({
    imports: [
        RegisterModule,
        LoginModule,
        SampleModule,
        QuestionsModule,
        EasyestimateModule,
        RefercaseModule,
        SettingsModule,
        MycasesModule

    ]
})
export class PagesModule
{

}
