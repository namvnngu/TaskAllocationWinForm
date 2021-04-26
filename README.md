## Task Allocation

__Description__: An application tests software related to a task allocation problem in which a parallel program is partitioned into a set of tasks and these tasks need to be allocated to a set of processors such the amount of energy consumed is minimised

__Unit__: SIT323 - Cloud Application Development

__University__: Deakin Unversity

__Technology__: Window Forms App (.NET Framework 4.8)

__IDE__: Visual Studio 2019

__How to run the application__:
1. Open `TasksAllocation.sln` solution
2. Press `Start` button to build and run the application
3. Once the application pops up, click `File` and then `Open` to open the TAFF file
4. If the allocation TAFF file and the configuration CFF file is valid, then the `Allocation` button in the `Validate` menu or the `Validate Allocation` button is enabled.
5. Click the `Allocation` button in the `Validate` menu or the `Validate Allocation` button to validate allocations

__How to run unit tests__:
1. Open `TasksAllocation.sln` solution
2. In the `Solution Explorer`, right click on `UnitTest` project, and then click on `Run Tests`

__Troubleshooting__:

If `MSTest.TestAdapter.2.1.1` and `MSTest.TestFramework.2.1.1` packages cannot be found in the `UnitTest` project, then follow the below steps:
1. Expand the `UnitTest` project
2. Right click on `References`
3. Click on `Manage NuGet Packages`
4. If the restore message appears, click on `Restore` button. Otherwise, then reinstall the above packages.
