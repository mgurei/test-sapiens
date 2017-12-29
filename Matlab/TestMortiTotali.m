%Test Computo totale dei cadaveri
Urtati = zeros(30,5);
Legnati = zeros (30, 5);
MortiAmmazzati = zeros(30,5);

for F = 1:5
for n = 1:30
Urtati(n,F) = MortiUrto2(3, F, 2, 0, 0, 50, 50,.5, .5, .6, .6,...
    300);


for Danno = 1:30
    

        
Legnati(Danno,F) = MortiArma2(.5, .5,F,Danno,300, 50 );
MortiAmmazzati = (Urtati + Legnati);
end
end
end



%% plot
plot(MortiAmmazzati)