import template from './navbar.html';
import controller from './navbar.controller';
import './_navbar.scss';

let navbarComponent = () => {
  return {
    restrict: 'E',
    scope: {},
    template,
    controller,
    controllerAs: 'vm',
    bindToController: true
  };
};

export default navbarComponent;
