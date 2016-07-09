const LOG = new WeakMap();
const MD_SIDENAV = new WeakMap();

class MainController {
  constructor($log, $mdSidenav) {
    'ngInject';

    let vm = this;
    LOG.set(vm, $log);
    MD_SIDENAV.set(vm, $mdSidenav);

    vm.name = 'root';
  }

  toggleLeft() {
    let vm = this;

    MD_SIDENAV.get(vm)('left')
      .toggle()
      .then(() => {
        LOG.get(vm).debug('toggle left side-nav is done');
      });
  }

  toggleRight() {
    let vm = this;

    MD_SIDENAV.get(vm)('right')
      .toggle()
      .then(() => {
        LOG.get(vm).debug('toggle right side-nav is done');
      });
  }
}

export default MainController;
