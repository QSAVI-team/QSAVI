function [x,y] = getdata(totalTime,COM_port)

x=[];
y=[];

%open serial comm
 if(nargin<1)
     disp('Need to specify the acquisition time - try again');
     return;
 elseif (nargin<2)     
     COM_port = 4;
     disp('Using default port COM4');
 end
 if ~isnumeric(COM_port)
     disp('The second input variable must be an integer between 2 and 8')
     return;
 elseif COM_port == 1
     disp('Cannot use COM1 - try again')
     return;
 elseif COM_port > 8
     disp('Com port number bust be 8 or less - try again')
 end
 comport = sprintf('COM%c',num2str(floor(COM_port)));

 s = serial(comport, 'BaudRate', 9600);
 fopen(s);
 set(s,'Tag','serial');
 set(s,'Timeout',5);

 pause(1);
 ii=1;
 flushinput(s);
 good =0;
 tic;
 t1 = 0;
 while t1 < totalTime
     if(s.BytesAvailable >1)
         rawData{ii}=fscanf(s,'%d,%d',[1,2]);
         if(good == 1)
             ii=ii+1;
         else
             good = 1;
         end
     end
    t1 = toc;
 end

 
 fclose(s);
 delete(s)
 clear s
num = cell2mat(rawData);
y = num(1:2:length(num)-1);
x = num(2:2:length(num));
x = x - x(1);
x = x.*1e-6;
if ~exist('Data','dir')
    mkdir Data
end
disp('Saving data in ''Data'' directory')
sv = 0;
while ~sv
    fname = input('Give filename; default is ''data.txt'' ','s');
    if length(fname) == 0
        fname = 'data.txt';
    end
    ffname = ['Data/' fname];
    sv = 1;
    if exist(ffname,'file')
        disp(['file ' ffname 'already exists'])
        answ = input('are you sure you want to overwrite it? ([y]/n) ','s');
        if answ ~= 'y'
            sv = 0;
        end
    end
end
ta = [x' y'];
eval(['save ' ffname ' ta -ascii -tabs']);
end