/* global malarkey:false, moment:false */

import Components from './components/components';

import { config } from './index.config';
import { runBlock } from './index.run';

angular.module('IFeelGoodSalon', [
  'ngAnimate',
  'ngAria',
  'ngCookies',
  'ngMessages',
  'ngMaterial',
  'ngResource',
  'ngSanitize',
  'toastr',
  'ui.router',

  Components.name
])
  .constant('malarkey', malarkey)
  .constant('moment', moment)
  .config(config)
  .run(runBlock);
