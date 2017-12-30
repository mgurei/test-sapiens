%Test Computo totale dei cadaveri

clear all; close all; clc;

F_ratio = 0.2:.1:5;

Urtati = zeros(1,length(F_ratio));
Legnati = zeros(30,length(F_ratio));
MortiAmmazzati = zeros(30,length(F_ratio));

for i = 1:length(F_ratio)
    F = F_ratio(i);
    for danno = 1:30
    Urtati(i) = MortiUrto2(3, F, 2, 0, 0, 50, 50,.5, .5, .6, .6,...
        300);
    Legnati(danno,i) = MortiArma2(.5, .5,F,danno,300, 50 );
    
    MortiAmmazzati(danno, i) = Urtati(i) + Legnati(danno, i);
    end
end


% plot
plot(F_ratio, MortiAmmazzati)
xlabel('F1/F2')
ylabel('Morti')