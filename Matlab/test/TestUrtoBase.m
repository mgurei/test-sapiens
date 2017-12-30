urtati = zeros(10,5);

for form = 1:5
for vel = 1:10
urtati(vel, form) = MortiUrto(vel, form, 1, 2, 0, 0, 50, 50,.5, .5, .6, .6, 300);
end
end

%% plot
plot(urtati)

