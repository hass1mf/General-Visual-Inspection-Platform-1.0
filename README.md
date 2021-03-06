# 通用工业检测平台1.0
起因是毕业设计选题通用软件开发，在市场上调研了一些工业软件，大部分非标是公司软件产品过于单一，只能应用于某种特定的场景下，而且软件的灵活性也不够，出现很多一个客户现场一个软件工程师，一个软件工程师一套软件框架，而且工业上的软件工程师编程水平普遍不高，出现了一个Project堆积了各种现场的无效代码，并产生了非常大的浪费和危害：

1.随着时间的积累代码量越来越庞大，维护的人也无法参与到工作中来,甚至需要花大量的时间熟悉工程和代码。

2.一个部门内始终没有一套统一的软件框架，最终造成各个软件工程师软件产品形态风格各异，软件交互逻辑完全不同，增加了公司内部运维的学习成本也增加了对终端客户的交付压力。

3.长此以往很难在一个公司内部形成有效技术积淀，同样的重复工作，工程师反复造轮子，编程效率之低。

4.团队无法协同配合，也无法通过git管理团队代码，遇到一些保密程度比较高的代码，必须由公司权限比较高工程师全程跟进，浪费人力，项目依赖性很强，经常发生项目核心负责人离开之后，项目面临烂尾的风险。

5.团队成员无法各司其职，现场常常是软件工程师必须肩负软件调试，算法方案，对招人的要求高，比如需要懂软件开发的同时必须还得懂图像处理，导致的后果就是只能招一些会一点软件开发又会一点图像处理的工程师，遇到难度比较大的项目就没有精力去攻克难题。

主客观因素都有，工业设备从业者基本上是机械或者电气转行过来，对待软件陷入思维定势，把结构设计和电气编程那套代入到工业软件开发中，造成了巨大的人力浪费，对软件的重视程度不高。

主次原因分析完之后，讲讲本项目，也调研过国内外一些比较不错的工业平台软件，比如NI,海康威视VisionMaster，百迈的visionPK，欧姆龙CX-Programmer，创科自动化CKVisionBuilder，康耐视的VisionPro，凌云VisionAssbmly AI等一些做得相当好的工业通用检测软件。在学习IOC设计模式之后，慢慢尝试把软件设计的思想运用在工业软件设计中。特色是可以通过注入DLL的方式来拓展软件的功能模块，从而不需要去对主体框架进行编译和修改代码，最大程度的解耦了业务和逻辑的关系，不知道能不能算MVC模式，尽量对整个软件各个部分进行解耦，因为软件在设计之初，就在考虑基于opencv的map数据结构还是halcon的hobject数据结构，后面思来想去，还是用了c#语言自带的bitmap数据结构（但也是犯了严重的问题，导致整体的运行速度拉长了很多，主要每个模块设计到太多的数据结构转换）。

本项目解决的是如何把功能模块和框架独立出来，所以软件交互设计并没有花太多形式，数据流的格式模仿了康耐视的Visionpro的方式（事实证明，这种方式可能是因为早期.net早期的语言特性导致的部分功能无法实现，才用了这种交互，主要是因为使得整个交互界面非常乱，而且卡顿现象也很严重包括vp自己的软件也会有这种情况，增加了运维成本也增加工控机的负担，说明有些东西存在的很久并不是一定是最优的，再后面的版本换了另外一种交互模式），整体的用户界面是找的网上devops的工业风设计，直接套过来用（UI不在本次软件设计中考虑）。

本项目主要是想提供一个思路为各位朋友提供另外一种编程思路，后续我会把整个运行环境打包，已经软件使用说明完善发出来。

主界面:
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/1.png)


软件根目录以及我们编译的存放Dll路径，通过注入DLL的方式拓展我们的功能模块其他详见代码：
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/6.png)
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/8.png)


工具栏拖拽出我们需要的模块：
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/2.png)


仿造的VisionPro的数据链路的交互模式：
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/3.png)


打开我们的图像采集工具（或则相机模块），导入图片：
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/4.png)

打开深度学习模块，把模型导入（深度学习是阿丘科技明星产品AIDI）：
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/5.png)


这个就是最终软件运行的效果：
![Alt text](https://github.com/hass1mf/General-Visual-Inspection-Platform-1.0/blob/main/image/9.png)


因为有很多AIDI的库已经软件用到的第三方类库，代码直接无法编译需要配置环境，需要的类库，上传百度网盘，有需要自取：

--来自百度网盘超级会员V5的分享
hi，这是我用百度网盘分享的内容~复制这段内容打开「百度网盘」APP即可获取 
链接:https://pan.baidu.com/s/1VjERv6jupvadKsV9h97ZQQ 
提取码:2hw8

