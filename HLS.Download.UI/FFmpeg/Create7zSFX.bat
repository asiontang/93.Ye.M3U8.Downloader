:: Ŀ�ģ�
:: 		Ϊ�˼�������������Խ�ѹ7Z������Ϊԭ ffmpeg.exe �ﵽ 60��MB��̫���ˡ�
:: 		���ھ���������С��14MB���ң�ͬʱ���޸н�ѹ��ִ�д��ݵĲ�����
:: 		��������ʱ������ȡ���̿ռ䡣
:: 
:: �ο����ϣ�
:: 		* [7z�����Խ�ѹ��װ��_Ľ���ּ�](https://www.imooc.com/article/28626) 
::			Ȼ����Ҫ���� 7zS2con.sfx �ļ���
::			7zs.sfx�ļ���7zΪ�����Խ�ѹ�İ�װ�����ṩ��һ���ļ���
::			9.20��7zs.sfx�ļ���7-Zip extra���У�֮��İ汾��������ļ�������LZMA���У����Ҹ���Ϊ7zs2.sfx
::			LZMA�����ص�ַ��https://www.7-zip.org/sdk.html
::			lzma.7z\Bin\7zS2.sfx    �ʺϾ���UI�ĳ��򣬼���������װ ffmpeg ������ʱ����������һ��CMD������ִ�ж������
::			lzma.7z\Bin\7zS2con.sfx �ʺ������г��򣬼���������װ ffmpeg ����ڵ�ǰCMD���ڼ���ִ�о���û�б�ѹ��һ��ִ�У�������ִ��ʱ�����루���ڽ�ѹ��
::
::			[7z/Installer at master �� sparanoid/7z �� GitHub](https://github.com/sparanoid/7z/tree/master/Installer) 
::			����� SFX ģ������ͨ�� SFX ģ�����ơ�������������
::			 - û�а�װ���������ļ�
::			 copy /b 7zS2.sfx + archive.7z sfx.exe
::			7zS2.sfx  small SFX module for installers (GUI version)  
::			7zS2con.sfx small SFX module for installers (Console version) 
::			�����Զ԰�װ�����Ͳ�������Ȼ��װ���Ὣ���Ǵ��͵�ָ���� .exe �ļ�
::			����� SFX ģ�����������ȼ������������ĸ��ļ����Զ�����
@COPY /B 7zS2con.sfx + ffmpeg.7z ffmpeg.exe