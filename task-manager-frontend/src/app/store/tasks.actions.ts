import {Task, tasksStore} from './tasks.store'

export const setTasks = (users : Task[]) => {
	tasksStore.update((state) => users);
}

export const getTasks = () => tasksStore.getValue();
