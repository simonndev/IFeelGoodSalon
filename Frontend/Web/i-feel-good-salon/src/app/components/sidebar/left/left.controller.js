const MD_SIDENAV = new WeakMap();
const LOG = new WeakMap();

class LeftSidebarController {
  constructor($mdSidenav, $log) {
    let vm = this;

    LOG.set(vm, $log);
    MD_SIDENAV.set(vm, $mdSidenav);

    vm.name = 'left-sidebar';
  }

  close() {
    let vm = this;

    MD_SIDENAV.get(vm)('left').close()
      .then(function () {
        LOG.get(vm).debug('close LEFT is done');
      });
  }

}

export default LeftSidebarController;
