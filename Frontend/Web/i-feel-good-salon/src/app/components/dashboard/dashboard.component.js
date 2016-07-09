import template from './dashboard.html';
import controller from './dashboard.controller';
import './_dashboard.scss';

let dashboardComponent = () => {
  return {
    restrict: 'E',
    scope: {},
    template,
    controller,
    controllerAs: 'vm',
    bindToController: true
  };
};

export default dashboardComponent;
